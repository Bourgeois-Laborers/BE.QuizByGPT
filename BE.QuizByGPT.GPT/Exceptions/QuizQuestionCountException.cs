namespace BE.QuizByGPT.GPT.Exceptions
{
    public class QuizQuestionCountLimitException : Exception
    {
        public QuizQuestionCountLimitException(string message) : base(message)
        {

        }
    }
}