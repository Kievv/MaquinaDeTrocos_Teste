using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaquinaDeTrocos.Models
{
    public class MaquinadeTrocoModel
    {

        public List<Moedas> moedas { get; set; }

        public double caixaTotal { get; set; }

        public static MaquinadeTrocoModel maquina { get; set; }

        public static MaquinadeTrocoModel getMaquina()
        {
           
            if(maquina == null)
            {

                maquina = new MaquinadeTrocoModel();

            }

            return maquina;

        }

        public void calcularTotal()
        {
            caixaTotal = 0;
            foreach(Moedas m in moedas)
            {
                caixaTotal += m.quantidade * m.valor;

            }
        }


        private MaquinadeTrocoModel()
        {
            string path = "~/Content/Imgs/";

            moedas = new List<Moedas>();

            Moedas umCentavo = new Moedas();

            umCentavo.nome = "umCent";
            umCentavo.quantidade = 0;
            umCentavo.valor = 1;
            umCentavo.srcIgm = path + umCentavo.nome + ".png";

            moedas.Add(umCentavo);

            Moedas cincoCentavos = new Moedas();
            cincoCentavos.nome = "cincoCent";
            cincoCentavos.valor = 5;
            cincoCentavos.quantidade = 0;
            cincoCentavos.srcIgm = path + cincoCentavos.nome + ".png";
            moedas.Add(cincoCentavos);

            Moedas dezCentavos = new Moedas();
            dezCentavos.nome = "dezCent";
            dezCentavos.valor = 10;
            dezCentavos.quantidade = 0;
            dezCentavos.srcIgm = path + dezCentavos.nome + ".png";
            
            moedas.Add(dezCentavos);

            Moedas vinteCincoCentavos = new Moedas();
            vinteCincoCentavos.nome = "vinteCincoCent";
            vinteCincoCentavos.valor = 25;
            vinteCincoCentavos.quantidade = 0;
            vinteCincoCentavos.srcIgm = path + vinteCincoCentavos.nome + ".png";

            moedas.Add(vinteCincoCentavos);

            Moedas cinquentaCentavos = new Moedas();
            cinquentaCentavos.nome = "cinquentaCent";
            cinquentaCentavos.valor = 50;
            cinquentaCentavos.quantidade = 0;
            cinquentaCentavos.srcIgm = path + vinteCincoCentavos.nome + ".png";
            moedas.Add(cinquentaCentavos);

            Moedas umReal = new Moedas();
            umReal.nome = "umReal";
            umReal.valor = 100;
            umReal.quantidade = 0;
            umReal.srcIgm = path + umReal.nome + ".png";

            moedas.Add(umReal);

        }

    }
}