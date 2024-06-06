using EnvDTE;
using System;

namespace ExamenMateoArguello
{
    public partial class Telefono : ContentPage
    {
        public Telefono()
        {
            InitializeComponent();
        }

        private void OnRecargaCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton.IsChecked)
            {
                GAMensajeRecarga.Text = $"Se ha seleccionado una recarga de: {radioButton.Content} dolares";
            }
        }


        private async void RecargarClicked(object sender, EventArgs e)
        {
            string numero = GATelefonoEntry.Text;
            string operador = GASelectionList.SelectedItem?.ToString();
            decimal count = 0;

            if (GARecarga3.IsChecked) count = 3;
            if (GARecarga5.IsChecked) count = 5;
            if (GARecarga10.IsChecked) count = 10;

            if (string.IsNullOrEmpty(numero) || numero.Length != 10 || !numero.All(char.IsDigit))
            {
                await DisplayAlert("Error", "El número telefónico debe contener un total de 10 digitos", "OK");
                return;
            }

            bool confirmacion = await DisplayAlert("Confirmación",
                                                   $"¿Está seguro de querer recargar {count} dólares al numero de teléfono: {numero}, con el operador {operador}?",
                                                   "Sí", "No");
            if (confirmacion)
            {
                string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                string contenido = $"Se ha realizado una recarga de ${count} dólares (Fecha: {fecha})";


                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", $"{numero}.txt");


                // Escribir el archivo
                File.WriteAllText(path, contenido);


                await DisplayAlert("Finalizado", $"La recarga de ${count} dólares se ha realizado exitosamente para el numero: {numero} y {operador}.", "Ok");
            }
            else
            {
                await DisplayAlert("Finalizado", "Recarga descartada", "Ok");
            }
        }
    }
}