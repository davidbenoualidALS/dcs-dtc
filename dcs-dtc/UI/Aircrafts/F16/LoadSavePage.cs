﻿using DTC.Models.Base;
using DTC.Models.F16;
using DTC.UI.CommonPages;
using System;
using System.Windows.Forms;

namespace DTC.UI.Aircrafts.F16
{
	public partial class LoadSavePage : AircraftSettingPage
	{
		private F16Configuration _mainConfig;
		private F16Configuration _configToLoad;

		public delegate void RefreshCallback();

		public LoadSavePage(AircraftPage parent, F16Configuration cfg) : base(parent)
		{
			_mainConfig = cfg;
			InitializeComponent();
		}

		public override string GetPageTitle()
		{
			return "Load / Save";
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			if (optClipboard.Checked)
			{
				var txt = Clipboard.GetText();
				_configToLoad = F16Configuration.FromCompressedString(txt);
			}
			else
			{
				if (openFileDlg.ShowDialog() == DialogResult.OK)
				{
					var file = FileStorage.LoadFile(openFileDlg.FileName);
					_configToLoad = F16Configuration.FromJson(file);
				}
			}

			DisableLoadControls();

			var enableLoad = false;

			if (_configToLoad != null)
			{
				if (_configToLoad.Waypoints != null)
				{
					chkLoadWaypoints.Enabled = true;
					enableLoad = true;
				}
				if (_configToLoad.CMS != null)
				{
					chkLoadCMS.Enabled = true;
					enableLoad = true;
				}
				if (_configToLoad.Radios != null)
				{
					chkLoadRadios.Enabled = true;
					enableLoad = true;
				}
				if (_configToLoad.MFD != null)
				{
					chkLoadMFDs.Enabled = true;
					enableLoad = true;
				}
				if (_configToLoad.HARM != null)
				{
					chkLoadHARM.Enabled = true;
					enableLoad = true;
				}
				if (_configToLoad.HTS != null)
				{
					chkLoadHTS.Enabled = true;
					enableLoad = true;
				}
				if (_configToLoad.Misc != null)
				{
					chkLoadMisc.Enabled = true;
					enableLoad = true;
				}

				if (enableLoad == true)
				{
					btnLoadApply.Enabled = true;
				}
			}
		}

		private void btnLoadApply_Click(object sender, EventArgs e)
		{
			var load = false;

			var cfg = _configToLoad.Clone();

			if (!chkLoadWaypoints.Checked)
			{
				cfg.Waypoints = null;
			}
			else
			{
				load = true;
			}

			if (!chkLoadCMS.Checked)
			{
				cfg.CMS = null;
			}
			else
			{
				load = true;
			}

			if (!chkLoadRadios.Checked)
			{
				cfg.Radios = null;
			}
			else
			{
				load = true;
			}

			if (!chkLoadMFDs.Checked)
			{
				cfg.MFD = null;
			}
			else
			{
				load = true;
			}

			if (!chkLoadHARM.Checked)
			{
				cfg.HARM = null;
			}
			else
			{
				load = true;
			}

			if (!chkLoadHTS.Checked)
			{
				cfg.HTS = null;
			}
			else
			{
				load = true;
			}

			if (!chkLoadMisc.Checked)
			{
				cfg.Misc = null;
			}
			else
			{
				load = true;
			}

			if (load)
			{
				_mainConfig.CopyConfiguration(cfg);
				_parent.RefreshPages();
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			var cfg = _mainConfig.Clone();

			if (!chkSaveWaypoints.Checked)
			{
				cfg.Waypoints = null;
			}
			if (!chkSaveCMS.Checked)
			{
				cfg.CMS = null;
			}
			if (!chkSaveRadios.Checked)
			{
				cfg.Radios = null;
			}
			if (!chkSaveMFDs.Checked)
			{
				cfg.MFD = null;
			}
			if (!chkSaveHARM.Checked)
			{
				cfg.HARM = null;
			}
			if (!chkSaveHTS.Checked)
			{
				cfg.HTS = null;
			}
			if (!chkSaveMisc.Checked)
			{
				cfg.Misc = null;
			}

			if (optClipboard.Checked)
			{
				Clipboard.SetText(cfg.ToCompressedString());
			}
			else
			{
				if (saveFileDlg.ShowDialog() == DialogResult.OK)
				{
					FileStorage.Save(cfg, saveFileDlg.FileName);
				}
			}
		}

		private void DisableLoadControls()
		{
			chkLoadWaypoints.Enabled = false;
			chkLoadCMS.Enabled = false;
			chkLoadRadios.Enabled = false;
			chkLoadMFDs.Enabled = false;
			chkLoadHARM.Enabled = false;
			chkLoadHTS.Enabled = false;
			chkLoadMisc.Enabled = false;
			btnLoadApply.Enabled = false;
		}

		private void optFile_CheckedChanged(object sender, EventArgs e)
		{
			_configToLoad = null;
			grpLoad.Text = "Load from File";
			grpSave.Text = "Save to File";
			grpLoad.Visible = true;
			grpSave.Visible = true;
			DisableLoadControls();
		}

		private void optClipboard_CheckedChanged(object sender, EventArgs e)
		{
			_configToLoad = null;
			grpLoad.Text = "Load from Clipboard";
			grpSave.Text = "Save to Clipboard";
			grpLoad.Visible = true;
			grpSave.Visible = true;
			DisableLoadControls();
		}
	}
}
