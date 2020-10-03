using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gestao.Service
{
    public class ValidaDataTarefa: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var tempoAgora =  DateTimeOffset.Now.Date;
            try
            {
                return (((DateTimeOffset)value).Date > tempoAgora);
            }
            catch
            {
                return false;
            }

            
        }
    }
}
