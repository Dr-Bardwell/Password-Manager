using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Password_Manager.DataTypes;

namespace WPF_Password_Manager
{
    /// <summary>
    /// PrimitiveButtonMethods.cs
    /// Hosts methods with the name Static in front.
    /// These methods do NOT interact with _eventHistory.
    /// Used by ButtonMethods.cs and DecryptDeed.cs
    /// StaticDelete(Container c);
    /// StaticAdd(string title,string data) || StaticAdd(Container c);
    /// StaticOverwriteContainer(Container c);
    /// StaticBack();
    /// StaticSelect(Container c);
    /// </summary>
    public partial class MainWindow : Window
    {
      /// <summary>
      /// StaticDelete: Takes container, deletes from tree
      /// Does NOT interact with _eventHistory
      /// </summary>
      private void StaticDelete(Container c)
      {
        string listname = "", item = "";
            var list = SelectedContainer.GetList();
            foreach (var item1 in list)
            {
                if (c.UniqueID == item1.UniqueID)
                {
                    c = item1;
                }
            }
            listname = c.Parent.Title;
            item = c.Title;
            SelectedContainer.Remove(c);
            SelectedContainer.EvaluteID();

            if (!String.IsNullOrEmpty(item)) //Tell user item successfully deleted
        {
            labelRecent.Text = $"'{item}' from '{listname}' successfully deleted!";
        }
        SaveCheck();
        //Refresh
        MenuHandler();
      }
      /// <summary>
      /// StaticAdd: Takes container, title and data, adds to tree
      /// Two versions, link nito eachother, so both are needed
      /// Does NOT interact with _eventHistory
      /// </summary>
      private void StaticAdd(string title,string data)
      {
        if (SelectedContainer.TitleCheck(title))
        {
            
            if (menuIndex != MenuLocation.Box)
            {
                StaticAdd(new Container(SelectedContainer.Count,title));
            }
            else
            {
                if (string.IsNullOrEmpty(data))
                    {
                        data = "----";
                    }
                StaticAdd(new Container(SelectedContainer.Count,title,data));
            }
        }
      }
      private void StaticAdd(Container c)
      {
            //add container
            labelRecent.Text = $"'{c.Title}' added to '{SelectedContainer.Title}'";
            SelectedContainer.Add(c);
            SelectedContainer.EvaluteID();
        //save
        SaveCheck();
        //refresh list
        MenuHandler();
      }
      /// <summary>
      /// StaticOverwriteContainer: Takes container, overwrites on tree
      /// Does NOT interact with _eventHistory
      /// </summary>
      private void StaticOverwriteContainer(Container c)
      {
        //ReplaceAt
        c.Parent.ReplaceAt(c);
        //save
        SaveCheck();
        //refresh list
        MenuHandler();
      }

      /// <summary>
      /// StaticBack: Moves up tree
      /// Does NOT interact with _eventHistory
      /// </summary>
      private void StaticBack()
      {
        try
        {
            labelRecent.Text = $"Went back from '{SelectedContainer.Title}'";
            SelectedContainer = SelectedContainer.Parent;
            switch (menuIndex)
            {
                case MenuLocation.Main:
                    //throw new Exception("no back on main"); //no back on main
                case MenuLocation.Container:
                    menuIndex = MenuLocation.Main;
                    break;
                case MenuLocation.Box:
                    menuIndex = MenuLocation.Container;
                    break;
            }
        }
        catch (Exception f)
        {
            Console.WriteLine($"\n\n\n{f.ToString()}");
        }
        //Refresh listViewer
        MenuHandler();
      }



      /// <summary>
      /// StaticAdd: Takes container, moves down tree
      /// Does NOT interact with _eventHistory
      /// </summary>
      private void StaticSelect(Container c)
      {
        try
        {
                SelectedContainer = c;
                if (menuIndex == MenuLocation.Main)
                {
                    InputSearch.Text = "";
                    labelRecent.Text = $"Container '{SelectedContainer.Title}' selected.";
                    menuIndex = MenuLocation.Container;
                }
                else if (menuIndex == MenuLocation.Container)
                {
                    InputSearch.Text = "";
                    labelRecent.Text = $"Box '{SelectedContainer.Title}' selected.";
                    menuIndex = MenuLocation.Box;
                }
        }
        catch (Exception e)
        {
          Console.WriteLine($"{e.ToString()}");
        }
        MenuHandler();
      }


      


    }
}
