using System.Diagnostics;
using System.Threading.Tasks;

namespace WindowsMemoryAnalyzer
{
   internal class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Hello, World!");

         ProcessAnalyzer analyzer = new ProcessAnalyzer();
         analyzer.AnalyzeProcess(
            "C:\\path\\to\\executable.exe",
            "command line arguments"
            );
      }
   }
}

class ProcessAnalyzer
{
   private Process myproc;
   public void AnalyzeProcess(string executable, string arguments)
   {
      myproc = new Process();
      myproc.StartInfo.FileName = executable;
      myproc.StartInfo.Arguments = arguments;
      myproc.EnableRaisingEvents = true;
      myproc.Exited += new EventHandler(PrintFinalStat);
      myproc.Start();
      for (int i = 0; i < 3000; i++)
      {
         Thread.Sleep(1000);
         myproc.Refresh();
         try
         {
            Console.WriteLine("Time passed since Process.Start() = " + (DateTime.Now - myproc.StartTime));
            Console.WriteLine("PrivateMemorySize64 = " + myproc.PrivateMemorySize64);
            Console.WriteLine("WorkingSet64 = " + myproc.WorkingSet64);
            Console.WriteLine("PeakWorkingSet64 = " + myproc.PeakWorkingSet64);
         }
         catch
         {
            break;
         }
         Console.WriteLine();
      }
   }

   private void PrintFinalStat(object sender, System.EventArgs e)
   {
      Console.WriteLine("Final time passed since Process.Start() = " + (DateTime.Now - myproc.StartTime));
      Console.WriteLine("Final TotalProcessorTime = " + myproc.TotalProcessorTime);
      Console.WriteLine("Final PrivateMemorySize64 = " + myproc.PrivateMemorySize64);
      Console.WriteLine("Final WorkingSet64 = " + myproc.WorkingSet64);
      Console.WriteLine("Final PeakWorkingSet64 = " + myproc.PeakWorkingSet64);
   }
}