/// <summary>
/// Interface định nghĩa hành vi cơ bản của vũ khí.
/// Tất cả các vũ khí trong game (gươm, cung, phép thuật, v.v.) cần phải triển khai interface này.
/// </summary>
public interface IWeapon
{
    /// <summary>
    /// Kích hoạt hành động tấn công của vũ khí.
    /// </summary>
    void Attack();

    /// <summary>
    /// Trả về thông tin của vũ khí (WeaponInfo).
    /// </summary>
    /// <returns>Thông tin của vũ khí</returns>
    WeaponInfo GetWeaponInfo();
}
