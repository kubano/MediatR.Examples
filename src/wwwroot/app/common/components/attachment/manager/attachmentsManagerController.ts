﻿/// <reference path="../../../../typings/_all.d.ts" />

module Antares.Common.Component.Attachment {
    import Dto = Common.Models.Dto;
    import Enums = Common.Models.Enums;
    import CartListOrder = Common.Component.ListOrder;

    export class AttachmentsManagerController {
        // bindings
        entityId: string;
        entityType: Enums.EntityTypeEnum;
        enumDocumentType: Dto.EnumTypeCode;
        onSaveAttachmentForEntity: (obj: { attachment: AttachmentUploadCardModel }) => ng.IPromise<Dto.IAttachment>;

        // controller
        attachmentsCartListOrder: CartListOrder = new CartListOrder('createdDate', true);

        isAttachmentPreviewPanelVisible: boolean;
        isAttachmentUploadPanelVisible: boolean;
        isAttachmentUploadPanelBusy: boolean = false;

        selectedAttachment: Common.Models.Business.Attachment = <Common.Models.Business.Attachment>{};

        constructor(
            private dataAccessService: Services.DataAccessService,
            private eventAggregator: Antares.Core.EventAggregator) {

            eventAggregator.with(this)
                .subscribe(Common.Component.CloseSidePanelEvent, () => {
                    this.isAttachmentUploadPanelVisible = false;
                    this.isAttachmentPreviewPanelVisible = false;
                });

            eventAggregator.with(this)
                .subscribe(Common.Component.BusySidePanelEvent, (event: Common.Component.BusySidePanelEvent) => {
                    this.isAttachmentUploadPanelBusy = event.isBusy;
                });
        }

        saveAttachment = (attachment: AttachmentUploadCardModel) => {
            return this.onSaveAttachmentForEntity({ attachment: attachment });
        }

        showAttachmentAdd = () => {
            if (this.isAttachmentPreviewPanelVisible === true) {
                this.isAttachmentPreviewPanelVisible = false;
            }

            this.isAttachmentUploadPanelVisible = true;
        }

        showAttachmentPreview = (attachment: Common.Models.Business.Attachment) => {
            if (this.isAttachmentUploadPanelVisible === true) {
                this.isAttachmentUploadPanelVisible = false;
            }

            this.selectedAttachment = attachment;
            this.isAttachmentPreviewPanelVisible = true;
        }
    }

    angular.module('app').controller('AttachmentsManagerController', AttachmentsManagerController);
}