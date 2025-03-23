using System.ComponentModel;

namespace StorekeeperAssistant.Domain.Movings;

public enum MovementType
{
    [Description("Приход")]
    Income = 1,
    [Description("Расход")]
    Expense = 2,
    [Description("Перемещение")]
    Moving = 3
}
