namespace ConstructionVR.Errors
{
    public class PropellerError : RotateZoneError
    {
        public override string GetErrorMessage()
        {
            return "Ошибка установки пропеллера номер ";
        }
    }
}
