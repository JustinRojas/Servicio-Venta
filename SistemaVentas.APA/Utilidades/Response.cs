namespace SistemaVentas.API.Utilidades
{
    /// <summary>
    /// Esta clase sirve para dar las resuestas en los controladores
    /// Note que es generica que recibe un modelo T
    /// y luego el metod value es de ese mismo tipo
    /// Ej Si se le pasa un objeto [Response<List<UsuarioDTO>>()] Value devuelve <List<UsuarioDTO>>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response<T>
    {
        public bool Status { get; set; }
        public T Value { get; set; }

        public string Sms { get; set; }

    }
}
