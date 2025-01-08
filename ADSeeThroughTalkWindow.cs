// Project:         Daggerfall Unity
// Copyright:       Copyright (C) 2009-2023 Daggerfall Workshop
// Web Site:        http://www.dfworkshop.net
// License:         MIT License (http://www.opensource.org/licenses/mit-license.php)
// Source Code:     https://github.com/Interkarma/daggerfall-unity
// Original Author: Lypyl (lypyldf@gmail.com), Gavin Clayton (interkarma@dfworkshop.net)
// Contributors:    Allofich, Hazelnut
//
// Notes:
//

using UnityEngine;
using Assets.MacadaynuMods.HiddenMapLocations;
using DaggerfallWorkshop.Game.UserInterface;

namespace DaggerfallWorkshop.Game.UserInterfaceWindows
{
    /// <summary>
    /// Spellbook UI for both casting spells and purchasing spells from guilds.
    /// </summary>
    public class ADSeeThroughTalkWindow : ADTalkWindow
    {
        protected Button buttonGoodbyeAlt;
        protected Button buttonLogbookAlt;

        public ADSeeThroughTalkWindow(IUserInterfaceManager uiManager, DaggerfallBaseWindow previous = null)
            : base(uiManager, previous)
        {
        }



        void SetupPanelTexture(int choice)
        {
            if (!isSetup)
                return;

            switch (choice)
            {
                case 1:
                    mainPanel.BackgroundTexture = ADDialogue.SmallTexture2D;
                    mainPanel.BackgroundColor = new Color(0, 0, 0, 0);
                    buttonGoodbyeAlt.Enabled = false;
                    buttonLogbookAlt.Enabled = false;
                    buttonGoodbye.Enabled = true;
                    buttonLogbook.Enabled = true;
                    break;
                case 2:
                    mainPanel.BackgroundTexture = ADDialogue.LargeTexture2D;
                    mainPanel.BackgroundColor = new Color(0, 0, 0, 0);
                    buttonGoodbyeAlt.Enabled = true;
                    buttonLogbookAlt.Enabled = true;
                    buttonGoodbye.Enabled = false;
                    buttonLogbook.Enabled = false;

                    break;
                default:
                    mainPanel.BackgroundTexture = ADDialogue.OriginalTexture2D;
                    mainPanel.BackgroundColor = new Color(0, 0, 0, 1);
                    buttonGoodbyeAlt.Enabled = false;
                    buttonLogbookAlt.Enabled = false;
                    buttonGoodbye.Enabled = true;
                    buttonLogbook.Enabled = true;
                    break;
            }
        }

		protected override void Setup()
		{
			base.Setup();
            buttonGoodbyeAlt = new Button();
            buttonGoodbyeAlt.Position = new Vector2(118, 185);
            buttonGoodbyeAlt.Size = new Vector2(67, 10);
            buttonGoodbyeAlt.Name = "button_goodbyeAlt";
            buttonGoodbyeAlt.BackgroundTexture = ADDialogue.buttonTexture;
            buttonGoodbyeAlt.Label.TextColor = Color.yellow;
            buttonGoodbyeAlt.Label.Text = "GoodBye";
            buttonGoodbyeAlt.OnMouseClick += ButtonGoodbye_OnMouseClick;
            buttonGoodbyeAlt.Hotkey = DaggerfallShortcut.GetBinding(DaggerfallShortcut.Buttons.TalkExit);
            buttonGoodbyeAlt.OnKeyboardEvent += ButtonGoodbye_OnKeyboardEvent;

            mainPanel.Components.Add(buttonGoodbyeAlt);

            buttonLogbookAlt = new Button
            {
                Position = new Vector2(118, 175),
                Size = new Vector2(67, 10),
                ToolTip = defaultToolTip,
                ToolTipText = TextManager.Instance.GetLocalizedText("copyLogbookInfo"),
            };
            if (defaultToolTip != null)
                buttonLogbookAlt.ToolTip.ToolTipDelay = 1;
            buttonLogbookAlt.Name = "button_logbookAlt";
            buttonLogbookAlt.BackgroundTexture = ADDialogue.buttonTexture;
            buttonLogbookAlt.Label.Text = "Copy To Logbook";
            buttonLogbookAlt.Label.TextColor = Color.yellow;
            buttonLogbookAlt.OnMouseClick += ButtonLogbook_OnMouseClick;
            buttonLogbookAlt.OnRightMouseClick += ButtonLogbook_OnRightMouseClick;
            // Can only assign one hotkey :(
            buttonLogbookAlt.Hotkey = DaggerfallShortcut.GetBinding(DaggerfallShortcut.Buttons.TalkCopy);
            mainPanel.Components.Add(buttonLogbookAlt);
            if (isSetup)
                SetupPanelTexture(ADDialogue.WindowChoice);

            
		}

        public override void SetNPCPortrait(FacePortraitArchive facePortraitArchive, int recordId)
        {
            if (!isSetup)
                return;

            if (ADDialogue.WindowChoice == 0)
            {
                base.SetNPCPortrait(facePortraitArchive, recordId);
                mainPanel.BackgroundColor = new Color(0, 0, 0, 1);

            }
            else
            {
                panelPortrait.BackgroundTexture = null;
                mainPanel.BackgroundColor = new Color(0, 0, 0, 0);
            }
            return;
        }

        public override void OnPush()
        {
            base.OnPush();
            if (isSetup)
                SetupPanelTexture(ADDialogue.WindowChoice);
        }
    }

}
