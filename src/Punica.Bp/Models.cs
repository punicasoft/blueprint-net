namespace Punica.Bp
{
    public record IdViewModel<T>(T Id);

    public record IdNameViewModel<T>(T Id, string Name);

    public record NameValueViewModel(string Name, string Value);
}
