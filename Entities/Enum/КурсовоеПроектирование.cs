using System.ComponentModel;

namespace Entities
{
    public enum КурсовоеПроектирование
    {
        [Description("КР")] КурсоваяРабота,
        [Description("КП")] КурсовойПроект,
        [Description("")] Нет,
        [Description("Ошибка")] Ошибка
    }
}