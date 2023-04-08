using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSQL.Scripts
{
    public enum EStatus : int
    {
        CRITICAL_ERROR = 1,
        LOAD_ERROR = 2,
        UNOWN_ERROR = 3,
        SUCCESS = 4,
    }
    public static class ExceptionTemplateCreator
    {
        public static string ProduceExceptionText(EStatus status)
        {
            switch (status)
            {
                case EStatus.CRITICAL_ERROR:
                    return "КРИТИЧЕСКАЯ ОШИБКА СИСТЕМЫ 1.";
                case EStatus.LOAD_ERROR:
                    return "ОШИБКА ЗАГРУЗКИ СИСТЕМЫ 2.";
                case EStatus.UNOWN_ERROR:
                    return "ОШИБКА НЕИЗВЕСТНОГО ПРОИСХОЖДЕНИЯ. ПАМЯТЬ ПРОГРАММЫ ПОВРЕЖДЕНА 3.";
                case EStatus.SUCCESS:
                    return "БАЗА ЗАГРУЖЕНА УСПЕШНО 4.";
                default:
                    return "ЭЙФОРИЙНАЯ ТЕНЬ УНИЧТОЖЕНА. СПАСИБО Д.С. И М.С. (ОТЧАСТИ), ЛЕТО 2021";
            }
        }
    }
}
