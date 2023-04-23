using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NetBank.Domain.Enums
{
    public enum TipoContaEnum
    {
        [Display(Name = "Conta Corrente")]
        ContaCorrente = 1,

        [Display(Name = "Conta Poupança")]
        ContaPoupanca = 2,
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }
    }
}
