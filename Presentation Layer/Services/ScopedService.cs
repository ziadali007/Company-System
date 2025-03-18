﻿
namespace Presentation_Layer.Services
{
    public class ScopedService : IScopedService
    {
        public ScopedService() 
        {
            Guid = Guid.NewGuid();
        }
        public Guid Guid { get  ; set  ; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
