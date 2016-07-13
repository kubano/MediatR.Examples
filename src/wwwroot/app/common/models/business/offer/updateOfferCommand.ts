﻿/// <reference path="../../../../typings/_all.d.ts" />

module Antares.Common.Models.Business {
    export class UpdateOfferCommand {
        id: string;
        statusId: string;
        price: number;
        pricePerWeek: number;
        offerDate: Date;
        exchangeDate: Date;
        completionDate: Date;
        specialConditions: string;
        searchStatusId: string;
        mortgageSurveyStatusId: string;
        mortgageStatusId: string;
        additionalSurveyStatusId: string;
        brokerId: string;
        brokerCompanyId: string;
        lenderId: string;
        lenderCompanyId: string;
        surveyorId: string;
        surveyorCompanyId: string;
        additionalSurveyorId: string;
        additionalSurveyorCompanyId: string;
        enquiriesId: string;
        contractApproved: boolean;
        mortgageLoanToValue: number;
        mortgageSurveyDate: Date | string;
        additionalSurveyDate: Date | string;
        progressComment: string;
        vendorSolicitorId: string = null;
        vendorSolicitorCompanyId: string = null;
        applicantSolicitorId: string = null;
        applicantSolicitorCompanyId: string = null;

        constructor(model?: Offer) {
            model = model || <Offer>{};
            this.id = model.id;
            this.statusId = model.statusId;
            this.price = model.price;
            this.pricePerWeek = model.pricePerWeek;
            this.offerDate = Core.DateTimeUtils.createDateAsUtc(model.offerDate);
            this.exchangeDate = Core.DateTimeUtils.createDateAsUtc(model.exchangeDate);
            this.completionDate = Core.DateTimeUtils.createDateAsUtc(model.completionDate);
            this.specialConditions = model.specialConditions;
            this.searchStatusId = model.searchStatusId;
            this.mortgageSurveyStatusId = model.mortgageSurveyStatusId;
            this.mortgageStatusId = model.mortgageStatusId;
            this.additionalSurveyStatusId = model.additionalSurveyStatusId;
            this.brokerId = model.brokerId;
            this.brokerCompanyId = model.brokerCompanyId;
            this.lenderId = model.lenderId;
            this.lenderCompanyId = model.lenderCompanyId;
            this.surveyorId = model.surveyorId;
            this.surveyorCompanyId = model.surveyorCompanyId;
            this.additionalSurveyorId = model.additionalSurveyorId;
            this.additionalSurveyorCompanyId = model.additionalSurveyorCompanyId;
            this.enquiriesId = model.enquiriesId;
            this.contractApproved = model.contractApproved;
            this.mortgageLoanToValue = model.mortgageLoanToValue;
            this.mortgageSurveyDate = Core.DateTimeUtils.createDateAsUtc(model.mortgageSurveyDate);
            this.additionalSurveyDate = Core.DateTimeUtils.createDateAsUtc(model.additionalSurveyDate);
            this.progressComment = model.progressComment;

            this.vendorSolicitorId = model.activity && model.activity.solicitorCompanyContact && model.activity.solicitorCompanyContact.contact ? model.activity.solicitorCompanyContact.contact.id : null;
            this.vendorSolicitorCompanyId = model.activity && model.activity.solicitorCompanyContact && model.activity.solicitorCompanyContact.company ? model.activity.solicitorCompanyContact.company.id : null;
            this.applicantSolicitorId = model.requirement && model.requirement.solicitorCompanyContact && model.requirement.solicitorCompanyContact.contact ? model.requirement.solicitorCompanyContact.contact.id : null;
            this.applicantSolicitorCompanyId = model.requirement && model.requirement.solicitorCompanyContact && model.requirement.solicitorCompanyContact.company ? model.requirement.solicitorCompanyContact.company.id : null;

            this.brokerId = model.brokerCompanyContact && model.brokerCompanyContact.contact.id;
            this.brokerCompanyId = model.brokerCompanyContact && model.brokerCompanyContact.company.id;
            this.lenderId = model.lenderCompanyContact && model.lenderCompanyContact.contact.id;
            this.lenderCompanyId = model.lenderCompanyContact && model.lenderCompanyContact.company.id;
            this.surveyorId = model.surveyorCompanyContact && model.surveyorCompanyContact.contact.id;
            this.surveyorCompanyId = model.surveyorCompanyContact && model.surveyorCompanyContact.company.id;
            this.additionalSurveyorId = model.additionalSurveyorCompanyContact && model.additionalSurveyorCompanyContact.contact.id;
            this.additionalSurveyorCompanyId = model.additionalSurveyorCompanyContact && model.additionalSurveyorCompanyContact.company.id;
        }

    }
}