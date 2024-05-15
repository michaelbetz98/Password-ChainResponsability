using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PasswordChainResponsability
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lenght = new Lenght();
            var nNumbers = new NNumbers();
            var upperCase = new UpperCase();
            var specialCharacter = new SpecialCharacter();

            var errorTracker = new ErrorTracker();


            lenght.SetSuccessor(nNumbers).SetSuccessor(upperCase).SetSuccessor(specialCharacter);

            Console.WriteLine("Write hte new password");
            string password = Console.ReadLine();
            ErrorTracker check = lenght.Control(password, errorTracker);
            Console.WriteLine(check.error);
            Console.WriteLine(check.valid);
            //Console.WriteLine(lenght.errors);
            //Console.WriteLine(lenght.aproved);
        }
    }

    public abstract class Approver
    {
        //public bool aproved = true;
        //public string errors = "";
        protected Approver? Next;

        public Approver SetSuccessor(Approver next)
        {
            //aproved = next.aproved;
            //errors = next.errors;
            Next = next;
            return Next;
        }

        public abstract ErrorTracker Control(string password, ErrorTracker errorTracker);
    }

    public class ErrorTracker
    {
        public string error {get;set;}
        public bool valid { get;set;}

        public ErrorTracker()
        {
            error = "";
            valid = true;
        }
    }

    public class Lenght : Approver
    {
        public override ErrorTracker Control(string password, ErrorTracker errorTracker)
        {
            if (password.Length >= 8)
            {
            }
            else
            {
                errorTracker.error+="The password must be at least 8 character long \n";
                errorTracker.valid = false;
            }
            
            var result = Next?.Control(password, errorTracker);
            return errorTracker;
        }
    }

    public class NNumbers : Approver
    {
        public override ErrorTracker Control(string password, ErrorTracker errorTracker)
        {
            if (password.Count(char.IsDigit)>=2)
            {
            }
            else
            {
               errorTracker.error+="The password must have at least 2 number \n";
               errorTracker.valid = false;
            }
            var result = Next?.Control(password, errorTracker);
            return errorTracker;
        }
    }

    public class UpperCase : Approver
    {
        public override ErrorTracker Control(string password, ErrorTracker errorTracker)
        {
            if (password.Any(char.IsUpper))
            {
            }
            else
            {
                errorTracker.error+=("The password must have at least 1 upper case letter \n");
                errorTracker.valid = false;
            }
            var result = Next?.Control(password, errorTracker);
            return errorTracker;
        }
    }

    public class SpecialCharacter : Approver
    {
        public override ErrorTracker Control(string password, ErrorTracker errorTracker)
        {
            var specialCharacters = "!@#$%^&*()-_+=[]{}|;:',.<>?/";
            if (password.Any(c => !char.IsLetterOrDigit(c) && specialCharacters.Contains(c)))
            {
            }
            else
            {
                errorTracker.error+=("The password must have at least 1 special character \n");
                errorTracker.valid = false;
            }
            var result = Next?.Control(password, errorTracker);
            return errorTracker;
        }
    }
}
