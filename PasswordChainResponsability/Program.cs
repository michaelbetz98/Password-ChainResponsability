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
            Console.WriteLine(lenght.errors);
            Console.WriteLine(lenght.aproved);
        }
    }

    public abstract class Approver
    {
        public bool aproved = true;
        public string errors = "";
        protected Approver? Next;

        public Approver SetSuccessor(Approver next)
        {
            //aproved = next.aproved;
            //errors = next.errors;
            Next = next;
            return Next;
        }

        public abstract (string error, bool valid) Control(string password);
    }

    public class Lenght : Approver
    {
        public override (string error, bool valid) Control(string password)
        {

            if (password.Length >= 8)
            {
                //Next?.Control(password);
            }
            else
            {
                errors="The password must be at least 8 character long \n" + errors;
                aproved = false;
            }
            
            var result = Next?.Control(password);
            if (result.HasValue)
            {
                errors += result.Value.Item1;
                aproved = result.Value.Item2;
            }
            return (errors, aproved);
        }
    }

    public class NNumbers : Approver
    {
        public override (string error, bool valid) Control(string password)
        {
            if (password.Count(char.IsDigit)>=2)
            {
                //Next?.Control(password);
            }
            else
            {
               errors+="The password must have at least 2 number \n";
               aproved = false;
            }
            var result = Next?.Control(password);
            if (result.HasValue)
            {
                errors += result.Value.Item1;
                aproved = result.Value.Item2;
            }
            return (errors, aproved);
        }
    }

    public class UpperCase : Approver
    {
        public override (string error, bool valid) Control(string password)
        {
            if (password.Any(char.IsUpper))
            {
                //Next?.Control(password);
            }
            else
            {
                errors+=("The password must have at least 1 upper case letter \n");
                aproved = false;
            }
            var result = Next?.Control(password);
            if (result.HasValue)
            {
                errors += result.Value.Item1;
                aproved = result.Value.Item2;
            }
            return (errors, aproved);
        }
    }

    public class SpecialCharacter : Approver
    {
        public override (string error, bool valid) Control(string password)
        {
            var specialCharacters = "!@#$%^&*()-_+=[]{}|;:',.<>?/";
            if (password.Any(c => !char.IsLetterOrDigit(c) && specialCharacters.Contains(c)))
            {
                //Next?.Control(password);
            }
            else
            {
                errors+=("The password must have at least 1 special character \n");
                aproved = false;
            }
            var result = Next?.Control(password);
            if (result.HasValue)
            {
                errors += result.Value.Item1;
                aproved = result.Value.Item2;
            }
            return (errors, aproved);
        }
    }
}
