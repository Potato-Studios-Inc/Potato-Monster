// Script for having a typewriter effect for UI
// Prepared by Nick Hwang (https://www.youtube.com/nickhwang)
// Want to get creative? Try a Unicode leading character(https://unicode-table.com/en/blocks/block-elements/)
// Copy Paste from page into Inpector

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeWriter : MonoBehaviour
{
	private Text _text;
	private TMP_Text _tmpProText;
	string _writer;
	public AudioSource audioSource;
	public GameObject ButtonCanvas;
	
	[SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;

	// Use this for initialization
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		_text = GetComponent<Text>()!;
		_tmpProText = GetComponent<TMP_Text>()!;

		if(_text != null)
        {
			_writer = _text.text;
			_text.text = "";

			StartCoroutine(nameof(TypeWriterText));
		}

		if (_tmpProText != null)
		{
			_writer = _tmpProText.text;
			_tmpProText.text = "";

			StartCoroutine(nameof(TypeWriterTMP));
		}
		
		audioSource.Play();
	}

	private IEnumerator TypeWriterText()
	{
		_text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in _writer)
		{
			if (_text.text.Length > 0)
			{
				_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
			}
			_text.text += c;
			_text.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if(leadingChar != "")
        {
			_text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
		}

		if (leadingChar == "")
		{
			audioSource.Stop();

		}
	}

	public void EnableButton()
    {
		ButtonCanvas.SetActive(true);
	}

	public IEnumerator TypeWriterTMP()
    {
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in _writer)
		{
			if (_tmpProText.text.Length > 0)
			{
				_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
			}
			_tmpProText.text += c;
			_tmpProText.text += leadingChar;

			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			_tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
		}
		if (leadingChar == "")
		{
			audioSource.Stop();
			EnableButton();
		}
	}
}
