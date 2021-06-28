#region

using System.Windows;
using System.Windows.Controls;

#endregion

namespace ToolIDE.Test.Chapter03
{
    public enum ProcessStage
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5
    }

    public class ProcessStageHelper : DependencyObject
    {
        public static readonly DependencyProperty ProcessCompletionProperty = DependencyProperty.RegisterAttached(
            "ProcessCompletion", typeof(double), typeof(ProcessStageHelper),
            new PropertyMetadata(0.0, OnProcessCompletionChanged));

        private static readonly DependencyPropertyKey ProcessStagePropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(
                "ProcessStage", typeof(ProcessStage), typeof(ProcessStageHelper),
                new PropertyMetadata(ProcessStage.Stage1));

        public static readonly DependencyProperty ProcessStageProperty = ProcessStagePropertyKey.DependencyProperty;

        private static void OnProcessCompletionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progress = (double) e.NewValue;
            var bar = d as ProgressBar;
            switch (progress)
            {
                case >= 0 and < 20:
                    bar?.SetValue(ProcessStagePropertyKey, ProcessStage.Stage1);
                    break;
                case >= 20 and < 40:
                    bar?.SetValue(ProcessStagePropertyKey, ProcessStage.Stage2);
                    break;
                case >= 40 and < 60:
                    bar?.SetValue(ProcessStagePropertyKey, ProcessStage.Stage3);
                    break;
                case >= 60 and < 80:
                    bar?.SetValue(ProcessStagePropertyKey, ProcessStage.Stage4);
                    break;
                case >= 80 and <= 100:
                    bar?.SetValue(ProcessStagePropertyKey, ProcessStage.Stage5);
                    break;
            }
        }

        public static void SetProcessCompletion(ProgressBar bar, double progress)
        {
            bar.SetValue(ProcessCompletionProperty, progress);
        }

        public static void SetProcessStage(ProgressBar bar, ProcessStage stage)
        {
            bar.SetValue(ProcessStagePropertyKey, stage);
        }
    }
}