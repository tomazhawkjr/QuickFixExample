using QuickFix.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLibrary.Extensions
{
    public static class QuickFixExtersions
    {
        public static string GetSymbol(this QuickFix.FIX44.NewOrderSingle order)
        {
            return order.Symbol != null ? order.Symbol.getValue() : string.Empty;
        }

        public static decimal GetPrice(this QuickFix.FIX44.NewOrderSingle order)
        {
            return order.Price != null ? order.Price.getValue() : 0;
        }

        public static char GetSide(this QuickFix.FIX44.NewOrderSingle order)
        {
            return order.Side != null ? order.Side.getValue() : Char.MinValue;
        }

        public static decimal GetOrderQty(this QuickFix.FIX44.NewOrderSingle order)
        {
            return order.OrderQty != null ? order.OrderQty.getValue() : 0;
        }

        public static int GetFatorCompraVenda(this QuickFix.FIX44.NewOrderSingle order)
        {
            var side = order.GetSide();

            if (side == Side.SELL)
                return -1;
            if (side == Side.BUY)
                return 1;

            return 1;
        }

        public static decimal GetValorTotal(this QuickFix.FIX44.NewOrderSingle order)
        {
            decimal valorTotal = (order.GetOrderQty() * order.GetPrice()) * GetFatorCompraVenda(order);
            return valorTotal;
        }

    }

}
