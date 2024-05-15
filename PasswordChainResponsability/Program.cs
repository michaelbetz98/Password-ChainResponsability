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


            lenght.SetSuccessor(nNumbers).SetSuccessor(upperCase).SetSuccessor(specialCharacter);
            Console.WriteLine("Write hte new password");
            string password = Console.ReadLine();
            lenght.Control(password);
        }
    }

    public abstract class Approver
    {
        protected Approver? Next;

        public Approver SetSuccessor(Approver next)
        {
            Next = next;
            return Next;
        }

        public abstract void Control(string password);
    }

    public class Lenght : Approver
    {
        public override void Control(string password)
        {
            if (password.Length >= 8)
            {
                Next?.Control(password);
            }
            else
            {
                Console.WriteLine("The password must be at least 8 character long");
            }
        }
    }

    public class NNumbers : Approver
    {
        public override void Control(string password)
        {
            if (password.Count(char.IsDigit)>=2)
            {
                Next?.Control(password); 
            }
            else
            {
                Console.WriteLine("The password must have at least 2 number");
            }
        }
    }

    public class UpperCase : Approver
    {
        public override void Control(string password)
        {
            if (!password.Any(char.IsUpper))
            {
                Next?.Control(password);
            }
            else
            {
                Console.WriteLine("The password must have at least 1 upper case letter");
            }
        }
    }

    public class SpecialCharacter : Approver
    {
        public override void Control(string password)
        {
            var specialCharacters = "!@#$%^&*()-_+=[]{}|;:',.<>?/";
            if (password.Any(c => !char.IsLetterOrDigit(c) && specialCharacters.Contains(c)))
            {
                Next?.Control(password);
            }
            else
            {
                Console.WriteLine("The password must have at least 1 special character");
            }
        }
    }
}
