using Spring.Helpers.Controls;
using Spring.Properties;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Spring.ViewControls.ViewHelpers
{
    public class PageManager
    {
        /// <summary>
        /// Main View Recurisve func [still in experiment mode]
        /// </summary>
        /// <param name="currentLeftTab">tabControlAdv1.TabPages[d]</param>
        /// <param name="rightPanel">panel5.treeViewAdv1</param>
        /// <param name="rightOptionsForCurrentTab">panel5.treeViewAdv1.Nodes</param>
        /// <param name="pageParent">tabPageAdvN</param>
        public void SetupMyView( TabPageAdv currentLeftTab,TreeViewAdv rightPanel, TreeNodeAdvCollection rightOptionsForCurrentTab, TabPageAdv pageParent,TreeNodeAdv leftpressednode)
        {
            if (leftpressednode.Enabled)
            for (int i = 0; i < PagesNodesNames.ALLLEFTPRIMARYTITLES.Count; i++)
            {
                if (currentLeftTab.Text.Equals(PagesNodesNames.BringFriendlyName(PagesNodesNames.ALLLEFTPRIMARYTITLES[i])))
                {
                    //now get the list of right nodes all with same i as key for the order
                    for(int p = 0;p<PagesNodesNames.ALLRIGHTTITLES.Count;p++)
                    {
                        if (p == i)
                        {
                                var icons_ = PagesNodesNames.RIGHTOPTIONSICONS[i].ToArray();
                            List<Syncfusion.Windows.Forms.Tools.TreeNodeAdv> AddOptions = new List<TreeNodeAdv>();
                            rightOptionsForCurrentTab.Clear();
                            //iterate for the names
                            for (int q = 0; q< PagesNodesNames.ALLRIGHTTITLES[p].ToArray().Length; q++)
                            {
                                var rightnodenames = PagesNodesNames.ALLRIGHTTITLES[p].ToArray();
                                AddOptions.Add(new Syncfusion.Windows.Forms.Tools.TreeNodeAdv() { Text = rightnodenames[q], LeftImageIndices = icons_[q] });

                            }
                            foreach(var righttab in AddOptions)
                            {
                                rightOptionsForCurrentTab.AddRange(new Syncfusion.Windows.Forms.Tools.TreeNodeAdv[] { righttab });
                            }

                            //visible event changing
                            currentLeftTab.VisibleChanged += (ws, r) => {
                                try
                                {
                                    if (currentLeftTab.TabVisible)
                                    {
                                        rightOptionsForCurrentTab.Clear();

                                        foreach (var righttab in AddOptions)
                                        {
                                            rightOptionsForCurrentTab.AddRange(new Syncfusion.Windows.Forms.Tools.TreeNodeAdv[] { righttab });
                                        }
                                    }
                                }
                                catch (Exception q) { }
                            };
                            //add controller
                            var view = PagesNodesNames.PagesViewsControls[i];
                            view.Dock = DockStyle.Fill;
                            //first add events to right options depending on which page will inhrites and set the view name
                            view.AddEventsToOptionsNodes(rightPanel); 
                            //then disable all option waiting for priviallges algorithm to determines which options will be active and whcich view.
                            view.DisableRightOptionsAndTestPrevilages(rightPanel);

                            pageParent.Controls.Add(view); 
                            //we set this image as defult one for these views not shown! [including non setted up and rules controlled]
                            pageParent.BackgroundImage = new Bitmap(Resources.not_available);

                            leftpressednode.Enabled = false;
                            view.Select();

                            currentLeftTab.Closed += (s, e) => {
                                //re-set all event again
                                rightPanel.BeforeSelect -= view.OptionsTree_BeforeSelect;

                                rightPanel.Click -= view.OptionsTree_Click;

                                leftpressednode.Enabled = true;
                            };
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }
}
