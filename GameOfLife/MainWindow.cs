using System;
using Gtk;
using GameOfLife;

public partial class MainWindow: Gtk.Window
{

	Life txtLife = new Life ();
	bool isRun = false;
	System.Timers.Timer tm = new System.Timers.Timer();
	int stepNum = 0;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();

		// define styles for the TextView
		TextTag tag = new TextTag("xxSmall");
		tag.Scale = Pango.Scale.XXSmall;
		gridview.Buffer.TagTable.Add (tag);

		tag = new TextTag ("monospace");
		tag.Family = "monospace";
		gridview.Buffer.TagTable.Add (tag);

		tag = new TextTag ("background");
		tag.Background = "#666666";
		gridview.Buffer.TagTable.Add (tag);

		tag = new TextTag ("foreground");
		tag.Foreground = "#999999";
		gridview.Buffer.TagTable.Add (tag);



		// print out the initial grid
		gridview.Buffer.Text = "";
		TextIter insertIter = gridview.Buffer.StartIter;
		gridview.Buffer.InsertWithTagsByName (ref insertIter, txtLife.printGrid (), new string[] {"foreground", "monospace"});
		// set the TextView background colour
		gridview.ModifyBase (StateType.Normal, new Gdk.Color (0x66, 0x66, 0x66));

		// set the timer
		tm.Interval = 200;
		tm.AutoReset = true;
		tm.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);

		labelStepNo.Text = "Step #" + stepNum.ToString();

	}






	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnQuitActionActivated (object sender, EventArgs e)
	{
		Application.Quit ();
	}


	protected void OnButton1Clicked (object sender, EventArgs e)
	{
		// if grid update is running
		if (isRun) {

			tm.Stop ();
			button1.Label = "Next Step";
			isRun = false;

		} else {

			tm.Start ();
			button1.Label = "Stop";
			isRun = true;
		}



	}

	// triggered event at the end of the timer count
	void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
	{
		txtLife.updateGrid ();
		gridview.Buffer.Text = "";
		TextIter insertIter = gridview.Buffer.StartIter;
		gridview.Buffer.InsertWithTagsByName (ref insertIter, txtLife.printGrid (), new string[] {"foreground", "monospace"});

		labelStepNo.Text = "Step #" + (++stepNum).ToString();
	}
}
