using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ApiFactura.Models.Request
{
    public class NewClientRequest
    {
        [Required]
        [MinLength(2, ErrorMessage = ("Debe tener mínimo 2 carácteres"))]
        public string name { get; set; }
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+))@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+))\.([A-Za-z]{2,})$", ErrorMessage = "Email is not valid")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [NewClientEmailExistsAttribute(ErrorMessage =
            "Email already exists")]
      

        public string email { get; set; }


        
        [Required(ErrorMessage = "Password Required")]
        [ValidarContraseña (ErrorMessage ="La contraseña debe tener mínimo una letra minúscula, mayÚscula, un carácter especial y un número")]
        public string password { get; set; }


        [Required]
        [ValidarCedula(ErrorMessage = "Cédula invalida")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Longitud de la cédula incorrecta")]
        [NewClientCedulaExistsAtrribute(ErrorMessage =
            "Cedula already exists")]
        public string cedula { get; set; }
    }

    #region Validations
    public class NewClientCedulaExistsAtrribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                using (var db = new Models.Context())
                {
                    var client = db.UserClients.Where(a => a.Cedula == value).FirstOrDefault();
                    if (client == null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
    public class NewClientEmailExistsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                using (var db = new Models.Context())
                {
                    var client = db.UserClients.Where(a => a.Email == value).FirstOrDefault();
                    if (client == null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }

    public class ValidarCedula : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            try
            {
                string cedula = value as string;

                // Verificar Provincia

                int prov = Convert.ToInt32(cedula.Substring(0, 2));
                if (prov > 24 || prov < 1)
                    return false;
                if (cedula.Length != 10)
                    return false;
                char[] digitos = cedula.ToCharArray();
                int sumaPar, sumaImpar, sumaTotal;
                sumaPar = sumaImpar = sumaTotal = 0;
                // Multiplar pos impares
                for (int i = 0; i < cedula.Length - 1; i += 2)
                {
                    int mul = Convert.ToInt32(digitos[i].ToString()) * 2;
                    sumaImpar += (mul > 9) ? mul - 9 : mul;
                }
                for (int i = 1; i < cedula.Length - 1; i += 2)
                {
                    sumaPar += Convert.ToInt32(digitos[i].ToString());
                }
                sumaTotal = sumaPar + sumaImpar;

                int decenaSuperior = (Convert.ToInt32(sumaTotal.ToString().Substring(0, 1)) + 1) * 10;
                int residuo = decenaSuperior - sumaTotal;
                int digitoVerificador = Convert.ToInt32(digitos[9].ToString());
                if (digitoVerificador != residuo)
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }

    public class ValidarContraseña : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            
            var lowerCaseRegex = "(?=.*[a-z])";
            var upperCaseRegex = "(?=.*[A-Z])";
            var symbolsRegex = @"(?=.*[!@#$%^&+/',\""*])";
            var numericRegex = "(?=.*[0-9])";



            if (!new Regex(@"^" + lowerCaseRegex + "").IsMatch(value.ToString()))
            {
                return false;
            }
            if (!new Regex(@"^" + upperCaseRegex + "").IsMatch(value.ToString()))
            {
                return false;
            }
            if (!new Regex(@"^" + symbolsRegex + "").IsMatch(value.ToString()))
            {
                return false;
            }
            if (!new Regex(@"^" + numericRegex + "").IsMatch(value.ToString()))
            {
                return false;
            }

            return true;

        }
        public class ValidarCorreo: ValidationAttribute
        {

        }
    }

    
}
    #endregion
