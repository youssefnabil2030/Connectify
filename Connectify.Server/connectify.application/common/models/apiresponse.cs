namespace connectify.application.common.models;

public class apiresponse<t>
{
    public bool is_success { get; set; }
    public string message { get; set; } = string.empty;
    public t? data { get; set; }

    public static apiresponse<t> success(t data, string message = "") =>
        new() { is_success = true, message = message, data = data };

    public static apiresponse<t> failure(string message) =>
        new() { is_success = false, message = message };
}
