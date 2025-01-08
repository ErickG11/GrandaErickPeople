using People.Models;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace People;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    public void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        App.PersonRepo.AddNewPerson(newPerson.Text);
        statusMessage.Text = App.PersonRepo.StatusMessage;
    }

    public void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        List<EGrandaPerson> people = App.PersonRepo.GetAllPeople();
        peopleList.ItemsSource = people;
    }

    public async void OnDeleteButtonClicked(object sender, EventArgs args)
    {
        var button = sender as Button;
        var person = button?.BindingContext as EGrandaPerson;

        if (person != null)
        {
            
            App.PersonRepo.DeletePerson(person);

            
            await DisplayAlert("Eliminado", $"{person.Name} acaba de eliminar un registro.", "OK");

        
            OnGetButtonClicked(sender, args);
        }
    }
}
