using Survey_Managment.ViewModel;

namespace Survey_Managment.Actions
{
    /// <summary>
    ///  Acciones de iteracion sobre la pregunta.
    /// </summary>
    public class QuestionActions
    {
        public ICollection<OptionModel>? Options { get; set; }

        public void PushOption() => Options.Add(new OptionModel() { Option = "" });

        public void QuitOption(OptionModel option) => Options.Remove(option);
    }
}
