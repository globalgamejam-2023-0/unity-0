﻿using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interogation : MonoBehaviour
{
    public TextMeshProUGUI adj1;
    public TextMeshProUGUI adj2;
    public TextMeshProUGUI adj3;
    public TextMeshProUGUI question;
    public Button button1;
    public Button button2;
    public Button button3;
    public int amountOfQuestions = 2;
    private bool clickyclick = false;
    
    private void Start()
    {
        ClueData cd = ClueSystem.cluesPlaced.First();
        if (ClueSystem.cluesPlaced.Count() < 1 || Statics.answeredQuestions < amountOfQuestions)
        {
            SceneManager.LoadScene("cancelled");
        }
        ClueSystem.cluesPlaced.RemoveAt(0);
        setupQuestion(cd);
        //List<ClueData> questions = ClueSystem.cluesPlaced;
        //int i = 0;
        //clickyclick = false;
        //while (i < amountOfQuestions && questions.Count() > 0)
        //{
        //    i++;
        //    setupQuestion(questions.First());
        //    questions.RemoveAt(0);
        //}

        //SceneManager.LoadScene("cancelled");
        //foreach (var clue in ClueSystem.cluesPlaced)
        //{
        //    setupQuestion(clue);
        //}
    }

    private void setupQuestion(ClueData cd)
    {
        Debug.Log($"Correct answer is: {cd.adjective}");
        //ClueData cd = ClueSystem.cluesPlaced.First();
        List<(bool, string)> cds = new();
        cds.Add((true, cd.adjective));
        cds.Add((false, cd.adjectives.First()));
        cd.adjectives.RemoveAt(0);
        cds.Add((false, cd.adjectives.First()));
        cds = cds.OrderBy(answer => Guid.NewGuid()).ToList();
        
        adj1.SetText(cds.First().Item2);
        setupButton(cds.First(), button1);
        cds.RemoveAt(0);
        
        adj2.SetText(cds.First().Item2);
        setupButton(cds.First(), button2);
        cds.RemoveAt(0);
        
        adj3.SetText(cds.First().Item2);
        setupButton(cds.First(), button3);
        cds.RemoveAt(0);
        
        question.SetText(cd.question);
    }

    private void setupButton((bool, string) answer, Button button)
    {
        if (answer.Item1)
        {
            button.onClick.AddListener(() =>
            {
                Debug.Log($"The answer was correct");
                SceneManager.LoadScene("questions");
                Statics.answeredQuestions++;
                //clickyclick = true;
                return;
                //SceneManager.LoadScene("cancelled");
                
                //SceneManager.LoadScene("Scenes/end_scenes/jailed");
            });
            return;
        }
        
        button.onClick.AddListener(() =>
        {
            Debug.Log($"The answer was incorrect");
            SceneManager.LoadScene("Scenes/end_scenes/jailed");
            clickyclick = true;
        });
    }
}