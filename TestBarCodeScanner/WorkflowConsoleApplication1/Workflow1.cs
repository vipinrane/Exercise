using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace WorkflowConsoleApplication1
{
	public sealed partial class Workflow1: SequentialWorkflowActivity
	{
        private int i;
        private DateTime start, finish;


		public Workflow1()
		{
			InitializeComponent();
		}

        private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        {
            //Console.WriteLine("Hello, World!");

            i = (new Random()).Next(15);
            Console.WriteLine("Number = " + i);
            Console.WriteLine("Started...");
            start = DateTime.Now;
        }

        private void codeActivity2_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Finished...");
            finish = DateTime.Now;
            Console.WriteLine("Time Elapsed : " + finish.Subtract(start));
            Console.WriteLine("my sequential activity");
            Console.ReadLine();
        }

        private void codeActivity3_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("IfElseBranch Activity1...");
        }

        private void codeActivity4_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("IfElseBranch Activity2...");
        }
	}

}
