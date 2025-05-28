// Copyright (c) GTAPoliceMods LLC. All Rights Reserved.
//
// All code and assets contained in this file are STRICTLY
// the intellectual property of GTAPoliceMods unless otherwise
// noted by a license provided. Do not copy or redistribute.

#pragma warning disable CS1591
#pragma warning disable SA1600

namespace FivePD.Common.Localization
{
    public class WarningLocalizations
    {
        public string ConfigException { get; set; } = "An error occured while parsing the {{path}} file, the default values will be used. Make sure that the JSON formatting is correct, we recommend using the following site to check it: https://jsonformatter.org/";

        public string EmptyConfigException { get; set; } = "The {{path}} file is empty, the default values will be used.";
    }
}