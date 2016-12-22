using System;
using System.Collections.Generic;
using EF.Entities;
using Interfaces;

namespace TypeMaps
{
    public class TypeMaps
    {
        public static Dictionary<Type,Type> InterfacesEntityDictionary = new Dictionary<Type, Type>()
        {
            {typeof (IAddresses), typeof (Addresses) }
        }; 
    }
}
