using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

/// <summary>
/// Extra-hashing and encrypting class by Verlemion
/// </summary>
public static class CuteHash {

	/// <summary>
	/// Encrypt the specified object and key.
	/// </summary>
	/// <param name="obj">Object.</param>
	/// <param name="key">Key.</param>
	public static string Encrypt(this object obj, string key) {
		string input = obj.ToString ();
		char[] output = new char[input.Length];

		for(int i = 0; i < input.Length; i++)
			output[i] = (char) (input[i] ^ key.ToCharArray()[i % key.Length]);

		byte[] jaja = Encoding.UTF8.GetBytes (new string (output));
		return Convert.ToBase64String (jaja);
	}

	/// <summary>
	/// Decrypt the specified input and key.
	/// </summary>
	/// <param name="input">Input.</param>
	/// <param name="key">Key.</param>
	public static string Decrypt(this string input, string key) {
		input = Encoding.UTF8.GetString (Convert.FromBase64String (input));
		char[] output = new char[input.Length];

		for(int i = 0; i < input.Length; i++)
			output[i] = (char) (input[i] ^ key.ToCharArray()[i % key.Length]);

		return new string (output);
	}

}
