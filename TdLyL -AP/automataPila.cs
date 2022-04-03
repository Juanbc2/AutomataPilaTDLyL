using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TdLyL__AP
{
    public class automataPila
    {
        public List<string> simbolosEntrada;
        public List<string> simbolosEnPila;
        private Stack<string> pila = new Stack<string>();
        public List<List<string>> transiciones;


        public automataPila(List<string> simbolosEntrada,List<string> simbolosEnPila,string confInicial)
        {
            this.simbolosEntrada = simbolosEntrada;
            this.simbolosEnPila = simbolosEnPila;
            setPila(confInicial);
        }

        //operación replace
        private void replace(List<string> elementos){
            pila.Pop();
            foreach (string str in elementos)
            {
                pila.Push(str);
            }
        }

        
        public void setPila(string confInicial)
        {
            pila.Clear();
            List<string> confPila = confInicial.Split(',').ToList();
            foreach (string str in confPila)
            {
                pila.Push(str);
            }
        }


        public string validarHilera(string str)
        {

            Queue<char> hilera = new Queue<char>();
            string procesoValidacion = "";
            foreach (char c in str){
                hilera.Enqueue(c);
            }
            try
            {
                while(hilera.Count>=0)
                {
                    bool done = false;
                    bool dequeue = true;
                    for (int i = 0; i < this.simbolosEnPila.Count; i++)
                    {
                        for (int j = 0; j < this.simbolosEntrada.Count; j++)
                        {
                            if (this.simbolosEnPila[i].Equals(this.pila.Peek()) && char.Parse(simbolosEntrada[j]).Equals(hilera.Peek()))
                            {
                                Console.WriteLine("Entra if, Transicion:" + transiciones[i][j]);
                                procesoValidacion += "\n Analizando caracter " + hilera.Peek();
                                switch (transiciones[i][j].Substring(0,1))
                                {
                                    case "F": procesoValidacion += ", Avanza por transición '" + transiciones[i][j]+"'"; break;
                                    case "A": pila.Push(hilera.Peek().ToString());procesoValidacion+=", Apila("+hilera.Peek()+") por transición '"+transiciones[i][j]+"'";break;
                                    case "D": procesoValidacion += ", Desapila(" + pila.Peek() + ") por transición '" + transiciones[i][j]+"' y retiene."; dequeue = false;pila.Pop(); break;
                                    case "T": procesoValidacion += ", Desapila(" + pila.Peek() + ") por transición '" + transiciones[i][j] + "' y avanza.";pila.Pop(); break;
                                    case "C": return procesoValidacion += ", Por transición '" + transiciones[i][j]+"', Hilera válida.";
                                    case "E": return procesoValidacion += ", Por Transición '" + transiciones[i][j]+"', Hilera inválida.";
                                    case "R": procesoValidacion += ", Remplaza(" + transiciones[i][j].Substring(1, transiciones[i][j].Length - 1) + ") por transición '" + transiciones[i][j].Substring(0,1) + "' y retiene."; this.replace(transiciones[i][j].Substring(1, transiciones[i][j].Length - 1).Split(',').ToList()); dequeue = false; break;//retener
                                    case "P": procesoValidacion += ", Remplaza(" + transiciones[i][j].Substring(1, transiciones[i][j].Length - 1) + ") por transición '" + transiciones[i][j].Substring(0,1) + "' y avanza."; this.replace(transiciones[i][j].Substring(1, transiciones[i][j].Length - 1).Split(',').ToList()); break;//avance
                                    default: Console.WriteLine("transicion desconocida"); break;
                                }
                                done = true;
                                break;
                            }
                        }
                        if (done) break;
                    }
                    if (dequeue)
                    {
                        hilera.Dequeue();
                    }
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.ToString());
                return ("Error de pila vacía, no puede desapilar más. \nPor tanto, la hilera no es válida.");
            }
            return "Error en el proceso";
        }
        
    }
}
