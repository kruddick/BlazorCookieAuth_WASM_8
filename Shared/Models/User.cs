namespace Shared.Models;
public class User
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Role { get; set; } = string.Empty;
	public string Token { get; set; } = string.Empty;
	public string RefreshToken { get; set; } = string.Empty;
	public DateTime RefreshTokenExpiryTime { get; set; } = DateTime.UtcNow.AddDays(7);
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
