using System;
using System.Collections.Generic;
using UnityEngine;

public class StepDataPersist : MonoBehaviour
{
    private TextLog textLog;
    public static StepDataPersist Instance { get; private set; }
    public Dictionary<string, List<Step>> StepsByCraftID { get; private set; } = new Dictionary<string, List<Step>>();
    public event Action<string> OnStepsUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // TextLog.Instance.Log("[SDP] Instantiated");
            LoadMockSteps();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void LoadStepsForCraft(string craftId, List<Step> steps)
    {
        if (!string.IsNullOrWhiteSpace(craftId) && steps != null)
        {
            StepsByCraftID[craftId] = steps;
            OnStepsUpdated?.Invoke(craftId);
        }
    }

    public List<Step> GetStepsForCraft(string craftId)
    {
        return StepsByCraftID.TryGetValue(craftId, out var steps) ? steps : new List<Step>();
    }

    //Mock Steps
    public void LoadMockSteps()
    {
        var demoSteps = new List<Step>
        {
            new Step { Step_ID = "1", Craft_ID = "C001", Step_Order = 1, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 1", Step_Instruction = "Start by preparing the base of your craft with a card", Timer_Duration = 300 },
            new Step { Step_ID = "2", Craft_ID = "C001", Step_Order = 2, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 2", Step_Instruction = "Boil Water", Timer_Duration = 5 },
            new Step { Step_ID = "3", Craft_ID = "C001", Step_Order = 3, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 3", Step_Instruction = "Drain noodles", Timer_Duration = 10 },
            new Step { Step_ID = "4", Craft_ID = "C001", Step_Order = 4, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 4", Step_Instruction = "Sauteed garlic in butter", Timer_Duration = 200 },
            new Step { Step_ID = "5", Craft_ID = "C001", Step_Order = 5, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 5", Step_Instruction = "Add Noodles", Timer_Duration = 0 },

            new Step { Step_ID = "6", Craft_ID = "C002", Step_Order = 1, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 1", Step_Instruction = "Drain noodles", Timer_Duration = 0 },

            new Step { Step_ID = "7", Craft_ID = "C003", Step_Order = 1, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 1", Step_Instruction = "Sauteed garlic in butter", Timer_Duration = 200 },
            new Step { Step_ID = "8", Craft_ID = "C003", Step_Order = 2, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 2", Step_Instruction = "Add Noodles", Timer_Duration = 0 },
            new Step { Step_ID = "9", Craft_ID = "C003", Step_Order = 3, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Step 2", Step_Instruction = "Add Noodles", Timer_Duration = 0 },

            new Step { Step_ID = "10", Craft_ID = "C004", Step_Order = 1, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Preparing the Salmon", Step_Instruction = "Preheat the oven to 350 F, arrange a sheet tray with parchment paper, place the salmon on the sheet tray, coat with olive oil, and bake for 15 minutes until flaky. Then, shred it with a fork and mix with soy sauce and 2 tablespoons Kewpie mayo.", Timer_Duration = 15 },
            new Step { Step_ID = "11", Craft_ID = "C004", Step_Order = 2, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Preparing the Rice", Step_Instruction = "Rinse sushi rice, bring the rinsed rice and water to a boil in a saucepan, then lower the heat to low, cover, and cook until fluffy and the water is absorbed, about 15 minutes. Whisk the vinegar, sugar, and salt together in a bowl. Add the cooked rice and furikake, and combine.", Timer_Duration = 15 },
            new Step { Step_ID = "12", Craft_ID = "C004", Step_Order = 3, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Assembling the Sushi Bake", Step_Instruction = "Press the seasoned rice into a 9-x11-inch or smaller baking dish. Press the saucy salmon on top of the rice. Drizzle the remaining 2 tablespoons Kewpie mayo and sriracha on top to coat. Sprinkle with sesame seeds and bake for 20 minutes.", Timer_Duration = 20 },
            new Step { Step_ID = "13", Craft_ID = "C004", Step_Order = 4, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Final Touches and Serving", Step_Instruction = "Remove the sushi bake from the oven, sprinkle with chopped scallions, and serve with sheets of nori.", Timer_Duration = 0 },

            new Step { Step_ID = "14", Craft_ID = "C005", Step_Order = 1, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Preparing the Salmon", Step_Instruction = "Preheat the oven to 350 F, arrange a sheet tray with parchment paper, place the salmon on the sheet tray, coat with olive oil, and bake for 15 minutes until flaky. Then, shred it with a fork and mix with soy sauce and 2 tablespoons Kewpie mayo.", Timer_Duration = 15 },
            new Step { Step_ID = "15", Craft_ID = "C005", Step_Order = 2, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Preparing the Rice", Step_Instruction = "Rinse sushi rice, bring the rinsed rice and water to a boil in a saucepan, then lower the heat to low, cover, and cook until fluffy and the water is absorbed, about 15 minutes. Whisk the vinegar, sugar, and salt together in a bowl. Add the cooked rice and furikake, and combine.", Timer_Duration = 15 },
            new Step { Step_ID = "16", Craft_ID = "C005", Step_Order = 3, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Assembling the Sushi Bake", Step_Instruction = "Press the seasoned rice into a 9-x11-inch or smaller baking dish. Press the saucy salmon on top of the rice. Drizzle the remaining 2 tablespoons Kewpie mayo and sriracha on top to coat. Sprinkle with sesame seeds and bake for 20 minutes.", Timer_Duration = 20 },
            new Step { Step_ID = "17", Craft_ID = "C005", Step_Order = 4, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Final Touches and Serving", Step_Instruction = "Remove the sushi bake from the oven, sprinkle with chopped scallions, and serve with sheets of nori.", Timer_Duration = 0 },

            new Step { Step_ID = "18", Craft_ID = "C006", Step_Order = 1, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Preparing the Salmon", Step_Instruction = "Preheat the oven to 350 F, arrange a sheet tray with parchment paper, place the salmon on the sheet tray, coat with olive oil, and bake for 15 minutes until flaky. Then, shred it with a fork and mix with soy sauce and 2 tablespoons Kewpie mayo.", Timer_Duration = 15 },
            new Step { Step_ID = "19", Craft_ID = "C006", Step_Order = 2, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Preparing the Rice", Step_Instruction = "Rinse sushi rice, bring the rinsed rice and water to a boil in a saucepan, then lower the heat to low, cover, and cook until fluffy and the water is absorbed, about 15 minutes. Whisk the vinegar, sugar, and salt together in a bowl. Add the cooked rice and furikake, and combine.", Timer_Duration = 15 },
            new Step { Step_ID = "20", Craft_ID = "C006", Step_Order = 3, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Assembling the Sushi Bake", Step_Instruction = "Press the seasoned rice into a 9-x11-inch or smaller baking dish. Press the saucy salmon on top of the rice. Drizzle the remaining 2 tablespoons Kewpie mayo and sriracha on top to coat. Sprinkle with sesame seeds and bake for 20 minutes.", Timer_Duration = 20 },
            new Step { Step_ID = "21", Craft_ID = "C006", Step_Order = 4, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Final Touches and Serving", Step_Instruction = "Remove the sushi bake from the oven, sprinkle with chopped scallions, and serve with sheets of nori.", Timer_Duration = 0 },

            new Step { Step_ID = "22", Craft_ID = "C007", Step_Order = 1, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Preparing the Salmon", Step_Instruction = "Preheat the oven to 350 F, arrange a sheet tray with parchment paper, place the salmon on the sheet tray, coat with olive oil, and bake for 15 minutes until flaky. Then, shred it with a fork and mix with soy sauce and 2 tablespoons Kewpie mayo.", Timer_Duration = 15 },
            new Step { Step_ID = "23", Craft_ID = "C007", Step_Order = 2, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Preparing the Rice", Step_Instruction = "Rinse sushi rice, bring the rinsed rice and water to a boil in a saucepan, then lower the heat to low, cover, and cook until fluffy and the water is absorbed, about 15 minutes. Whisk the vinegar, sugar, and salt together in a bowl. Add the cooked rice and furikake, and combine.", Timer_Duration = 15 },
            new Step { Step_ID = "24", Craft_ID = "C007", Step_Order = 3, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Assembling the Sushi Bake", Step_Instruction = "Press the seasoned rice into a 9-x11-inch or smaller baking dish. Press the saucy salmon on top of the rice. Drizzle the remaining 2 tablespoons Kewpie mayo and sriracha on top to coat. Sprinkle with sesame seeds and bake for 20 minutes.", Timer_Duration = 20 },
            new Step { Step_ID = "25", Craft_ID = "C007", Step_Order = 4, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C004-2.mp4"), Title = "Final Touches and Serving", Step_Instruction = "Remove the sushi bake from the oven, sprinkle with chopped scallions, and serve with sheets of nori.", Timer_Duration = 0 },
            new Step { Step_ID = "3", Craft_ID = "C003", Step_Order = 1, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C003-1.mp4"), Title = "Pattern Making", Step_Instruction = "Create a pattern for the wallet using posterboard, measuring a rectangle 9.5 inches by 4.25 inches and marking fold lines.", Timer_Duration = 1200 },
            new Step { Step_ID = "4", Craft_ID = "C003", Step_Order = 2, Step_Video = System.IO.Path.Combine(Application.streamingAssetsPath, "C003-2.mp4"), Title = "Cutting the Leather", Step_Instruction = "Lay the pattern on the leather and mark the outline. Cut the leather using a rolling cutter and steel ruler.", Timer_Duration = 600 },


        };

        foreach (var step in demoSteps)
        {
            if (!StepsByCraftID.ContainsKey(step.Craft_ID))
            {
                StepsByCraftID[step.Craft_ID] = new List<Step>();
            }
            StepsByCraftID[step.Craft_ID].Add(step);
        }
    }

    public List<Step> GetStepsForCurrentCraft(string craftId)
    {
        if (StepsByCraftID.TryGetValue(craftId, out List<Step> steps))
        {
            return steps;
        }
        return null; // or return an empty list if that's preferable
    }
}