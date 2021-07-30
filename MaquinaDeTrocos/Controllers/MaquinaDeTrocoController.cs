using MaquinaDeTrocos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MaquinaDeTrocos.Controllers
{
    public class MaquinaDeTrocoController : Controller
    {
        MaquinadeTrocoModel maquina = MaquinadeTrocoModel.getMaquina();

        // GET: MaquinaDeTroco
        public ActionResult Index()
        {
            novasMoedas moedasDois = new novasMoedas();

            moedasDois.moedasList = maquina.moedas;

            foreach(Moedas m in moedasDois.moedasList)
            {
                m.quantidade = 0;
            }
          
            return View("abastecer", moedasDois);
        }

        [HttpPost]
        public ActionResult addMoedas(novasMoedas moedasDois)
        {

            for (int i = 0; i < moedasDois.moedasList.Count; i++)
            {
                maquina.moedas[i].quantidade += moedasDois.moedasList[i].quantidade;
            }
            maquina.calcularTotal();


            //ViewBag.message = "Máquina Abastecida!";

            //moedasDois.moedasList = maquina.moedas;

            //foreach (Moedas m in moedasDois.moedasList)
            //{
            //    m.quantidade = 0;
            //}

            JsonResult retorno = Json(new { 
            sucesso = "Deu certo!",
            valores = maquina.moedas,
            mensagem = "Tmj"
            }, JsonRequestBehavior.AllowGet);

            //return  ("abastecer", moedasDois);

            return retorno;

        }

        public ActionResult valorTroco()
        {
            return View("Troco");
        }

        [HttpPost]
        public ActionResult calcularTroco(Teste x)
        {
            JsonResult retorno;

            double vReceb = Convert.ToDouble(x.recebido);
            double vProd = Convert.ToDouble(x.produtos);

            List<Moedas> troco = new List<Moedas>();
            List<Moedas> moedaList = new List<Moedas>();

            moedaList = maquina.moedas;

            double restante = vReceb - vProd;

            if ((maquina.caixaTotal - restante) >= 0)
            {

                for (int i = 0; i < maquina.moedas.Count(); i++)
                {
                    Moedas moeda = new Moedas();
                    

                    moeda.quantidade = 0;
                    moeda.valor = moedaList[i].valor;
                    troco.Add(moeda);

                    if ((restante / moedaList[i].valor) > 0)
                    {
                        int quantidadeNecessaria = Convert.ToInt32(restante) / moedaList[i].valor;


                        if (moedaList[i].quantidade >= quantidadeNecessaria)
                        {
                            restante -= quantidadeNecessaria * moedaList[i].valor;

                            troco[i].quantidade = quantidadeNecessaria;

                        }

                        else
                        {
                            if (moedaList[i].quantidade > 0)
                            {
                                restante -= moedaList[i].quantidade * moedaList[i].valor;

                                troco[i] = moedaList[i];

                                
                            }
                        }

                    }


                }

                if (restante != 0)
                {
                    retorno = Json(new
                    {
                        mensagem = "Deu errado!",
                        erro = "Quantidade de moedas insuficiente para realizar a operação"
                    }, JsonRequestBehavior.AllowGet);
                    

                }

                else
                {

                    for (int i = 0; i < moedaList.Count(); i++)
                    {
                        maquina.moedas[i].quantidade -= troco[i].quantidade;
                    }
                    maquina.calcularTotal();

                    retorno = Json(new
                    {
                        mensagem = "Deu certo",
                        sucesso = "Maquina atualizada com sucesso"
                    }, JsonRequestBehavior.AllowGet);
                }

            }

            else
            {
                retorno = Json(new
                {
                    mensagem = "Deu errado!",
                    erro = "Quantidade de moedas insuficiente para realizar a operação"
                }, JsonRequestBehavior.AllowGet);

            }
            return retorno;
        }


            
        

        public class Teste {

            public string recebido { get; set; }

            public string produtos { get; set; }

        }

        
    }
}