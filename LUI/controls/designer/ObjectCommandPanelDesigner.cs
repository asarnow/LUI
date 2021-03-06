﻿using System.Windows.Forms.Design;

namespace LUI.controls.designer
{
    class ObjectCommandPanelDesigner : ParentControlDesigner
    {
        public override void Initialize(System.ComponentModel.IComponent Component)
        {
            base.Initialize(Component);
            if (this.Control is ObjectCommandPanel)
            {
                this.EnableDesignMode(((ObjectCommandPanel)this.Control).Flow, "Flow");
            }
        }
    }
}
