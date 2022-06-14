﻿// --------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ---------------------------------------------------------------

using Bunit;
using DMX.Portal.Web.Models.Views.LabViews;
using DMX.Portal.Web.Views.Components.DeviceOverviews;
using FluentAssertions;
using Xunit;

namespace DMX.Portal.Web.Tests.Unit.Components.DeviceOverviews
{
    public partial class DeviceOverviewComponentTests : TestContext
    {
        [Fact]
        public void ShouldHaveDefaultValues()
        {
            // given . when
            var initialDeviceOverviewComponent =
                new DeviceOverviewComponent();

            // then
            initialDeviceOverviewComponent.Device
                .Should().BeNull();

            initialDeviceOverviewComponent.ImageUrl
                .Should().BeNull();

            initialDeviceOverviewComponent.PowerLevelImageUrl
                .Should().BeNull();

            initialDeviceOverviewComponent.DeviceLabel
                .Should().BeNull();

            initialDeviceOverviewComponent.Image
                .Should().BeNull();

            initialDeviceOverviewComponent.PowerLevelImage
                .Should().BeNull();
        }

        [Fact]
        public void ShouldRenderDeviceName()
        {
            // given
            LabDeviceView randomLabDeviceView =
                CreateRandomLabDeviceView();

            LabDeviceView inputLabDeviceView =
                randomLabDeviceView;

            string expectedDeviceName = inputLabDeviceView.Name;

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(DeviceOverviewComponent.Device),
                    value: inputLabDeviceView);

            // when
            this.renderedDeviceOverviewComponent =
                RenderComponent<DeviceOverviewComponent>(inputComponentParameter);

            // then
            this.renderedDeviceOverviewComponent.Instance.DeviceLabel.Text
                .Should().BeEquivalentTo(expectedDeviceName);
        }

        [Theory]
        [MemberData(nameof(AllDeviceImages))]
        public void ShouldRenderDeviceImage(
            (LabDeviceTypeView LabDeviceTypeView, string Url) deviceTypeImage)
        {
            // given
            LabDeviceView randomLabDeviceView =
                CreateRandomLabDeviceView();

            LabDeviceView inputLabDeviceView =
                randomLabDeviceView;

            inputLabDeviceView.Type = deviceTypeImage.LabDeviceTypeView;
            string expectedImagePath = deviceTypeImage.Url;

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(DeviceOverviewComponent.Device),
                    value: inputLabDeviceView);

            // when
            this.renderedDeviceOverviewComponent =
                RenderComponent<DeviceOverviewComponent>(inputComponentParameter);

            // then
            this.renderedDeviceOverviewComponent.Instance.Image.Url
                .Should().BeEquivalentTo(expectedImagePath);
        }

        [Theory]
        [MemberData(nameof(AllDevicePowerLevelImages))]
        public void ShouldRenderDevicePowerLevel(
            (PowerLevelView Type, string Url) devicePowerLevel)
        {
            // given
            LabDeviceView randomLabDeviceView =
                CreateRandomLabDeviceView();

            LabDeviceView inputLabDeviceView =
                randomLabDeviceView;

            inputLabDeviceView.PowerLevel = devicePowerLevel.Type;
            string expectedPowerLevelImageUrl = devicePowerLevel.Url;

            ComponentParameter inputComponentParameter =
                ComponentParameter.CreateParameter(
                    name: nameof(DeviceOverviewComponent.Device),
                    value: inputLabDeviceView);

            // when
            this.renderedDeviceOverviewComponent =
                RenderComponent<DeviceOverviewComponent>(inputComponentParameter);

            // then
            this.renderedDeviceOverviewComponent.Instance.PowerLevelImage.Url
                .Should().BeEquivalentTo(expectedPowerLevelImageUrl);
        }
    }
}