using System.Linq;

namespace Domain.Common
{
    public abstract class Person : Entity
    {
        public string Name { get; protected set; }

        public string CPF { get; protected set; }

        protected Person(string name, string cpf)
        {
            Name = name;
            CPF = cpf;
        }

        protected bool ValidateName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            var words = Name.Split(' ');
            if (words.Length < 1)
            {
                return false;
            }

            foreach (var word in words)
            {
                if (word.Trim().Length < 2)
                {
                    return false;
                }
                if (word.Any(x => !char.IsLetter(x)))
                {
                    return false;
                }
            }
            return true;
        }

        protected bool ValidateCPF()
        {
            if (string.IsNullOrEmpty(CPF))
            {
                return false;
            }

            if (CPF.Length != 11)
            {
                return false;
            }

            if (!CPF.All(char.IsNumber))
            {
                return false;
            }

            var first = CPF[0];
            if (CPF.Substring(1, 10).All(x => x == first))
            {
                return false;
            }

            int[] multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp;
            string digit;
            int sum;
            int rest;

            temp = CPF.Substring(0, 9);
            sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(temp[i].ToString()) * multiplier1[i];
            }

            rest = sum % 11;

            rest = rest < 2 ? 0 : 11 - rest;

            digit = rest.ToString();
            temp += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(temp[i].ToString()) * multiplier2[i];
            }

            rest = sum % 11;

            rest = rest < 2 ? 0 : 11 - rest;

            digit += rest.ToString();

            if (CPF.EndsWith(digit))
            {
                return true;
            }

            return false;
        }
    }
}
