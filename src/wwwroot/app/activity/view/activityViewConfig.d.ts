﻿/// <reference path="../../typings/_all.d.ts" />

declare module Antares.Activity {
    import Attributes = Antares.Attributes;

    interface IActivityViewConfig extends IActivityConfig {
        departments: Attributes.IActivityDepartmentsViewControlConfig;
        vendors: Attributes.IActivityVendorsControlConfig;
        landlords: Attributes.IActivityLandlordsControlConfig;
        negotiators: Attributes.IActivityNegotiatorsControlConfig;
        property: Attributes.IPropertyViewControlConfig;
        priceType: any;
        activityPrice: any;
        matchFlexValue: any;
        matchFlexPercentage: any;
        shortAskingMonthRent: any;
        shortAskingWeekRent: any;
        shortMatchFlexMonthValue: any;
        shortMatchFlexWeekValue: any;
        shortMatchFlexPercentage: any;
        longAskingMonthRent: any;
        longAskingWeekRent: any;
        longMatchFlexMonthValue: any;
        longMatchFlexWeekValue: any;
        longMatchFlexPercentage: any;
    }
}