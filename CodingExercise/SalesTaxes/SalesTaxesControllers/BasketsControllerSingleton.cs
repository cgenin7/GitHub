using System;
using System.Collections.Generic;
using SalesTaxesModels;
using System.Diagnostics;

namespace SalesTaxesControllers
{
    public class BasketsControllerSingleton
    {
        private static BasketsControllerSingleton m_controller = null;
        private static object m_lockObject = new object();

        private BasketsControllerSingleton()
        { }

        public static BasketsControllerSingleton Controller
        {
            get 
            {
                lock (m_lockObject)
                {
                    if (m_controller == null)
                        m_controller = new BasketsControllerSingleton();
                }
                return m_controller;
            }
        }
    }
}
