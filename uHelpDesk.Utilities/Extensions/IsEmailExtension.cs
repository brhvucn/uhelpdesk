using EnsureThat;

namespace uHelpDesk.Utilities.Extensions
{
    public static class IsEmailExtension
    {
        public static StringParam IsEmail(this StringParam param)
        {
            if (!string.IsNullOrWhiteSpace(param.Value))
            {
                if (RegExUtilities.IsValidEmail(param.Value))
                {
                    return param;
                }
                else
                {
                    throw Ensure.ExceptionFactory.ArgumentException("Invalid email address", param.Name);
                }
            }
            else
            {
                return param;
            }
        }
    }
}
