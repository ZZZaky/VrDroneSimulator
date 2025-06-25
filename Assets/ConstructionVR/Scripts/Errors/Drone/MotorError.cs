namespace ConstructionVR.Errors
{
    public class MotorError : RotateZoneError
    {
        public override string GetErrorMessage()
        {
            return "Ошибка установки двигателя номер ";
        }
    }
}
