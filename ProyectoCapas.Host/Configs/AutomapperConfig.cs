using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ProyectoCapas.Host.Mapping;

namespace ProyectoCapas.Host.Configs
{
    public class AutomapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new TiendaProfile());
            });
        }
    }
}