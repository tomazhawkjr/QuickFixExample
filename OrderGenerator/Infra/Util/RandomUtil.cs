using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderGenerator.Infra.Util
{
    public static class RandomUtil
    {
        public static int GetRandomInt(int maxLenght, int? minValue = 1)
        {
            Random random = new Random();
            
            if(minValue.HasValue)
                return random.Next(minValue.Value, maxLenght);

            return random.Next(maxLenght);
        }

        public static decimal GetRandomDecimalMinMax(decimal valorMinimo, decimal valorMaximo)
        {
        
            Random random = new Random();           
            decimal valorDecimalAleatorio = (decimal)random.NextDouble();           
            valorDecimalAleatorio = valorMinimo + ((valorMaximo - valorMinimo) * valorDecimalAleatorio);

            return Decimal.Round(valorDecimalAleatorio, 2);
        }

        public static T GetRandomItemList<T>(List<T> list)
        {                      
           return list[GetRandomInt(list.Count, 0)];
        }
    }
}
