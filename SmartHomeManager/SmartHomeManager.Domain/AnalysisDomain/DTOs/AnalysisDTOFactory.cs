using System;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.DTOs;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.DTOs;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
	public class AnalysisDTOFactory : AbstractDTOFactory
	{
        public override ResponseDTO CreateResponseDTO(ResponseDTOType type, IEnumerable<IEntity> data, int statusCode, string statusMessage)
        {
            switch (type)
            {
                case ResponseDTOType.DEVICE_GETBYACCOUNT:
                    return CreateGetDevicesDTO((IEnumerable<Device>)data, statusCode, statusMessage);

                case ResponseDTOType.ANALYSIS_CARBONFOOTPRINT_GETBYACCOUNTMONTHYEAR:
                    return CreateGetCarbonfootprintDTO((IEnumerable<CarbonFootprint>)data, statusCode, statusMessage);

                case ResponseDTOType.ANAYLSIS_FORECAST_GETBYACCOUNTTIMESPAN:
                    return CreateGetForecastChartDataDTO((IEnumerable<ForecastChartData>)data, statusCode, statusMessage);
                default:
                    return null;
            }
        }

        public GetDevicesDTO CreateGetDevicesDTO(
            IEnumerable<Device> devices,
            int statusCode,
            string statusMessage
        )
        {
            List<GetDevicesDTOData> getDevices = new List<GetDevicesDTOData>();

            if (devices != null)
            {
                foreach (var device in devices)
                {
                    getDevices.Add(new GetDevicesDTOData
                    {
                        DeviceID = device.DeviceId,
                        DeviceName = device.DeviceName,
                    });

                }
            }
            return new GetDevicesDTO
            {
                Data = getDevices,
                Response = new ResponseInformation
                {
                    ServerMessage = statusMessage,
                    StatusCode = statusCode
                }
            };
        }

        public GetCarbonFootprintDTO CreateGetCarbonfootprintDTO(
            IEnumerable<CarbonFootprint> carbonFootprints,
            int statusCode,
            string statusMessage
        )
        {
            List<GetCarbonFootprintDTOData> getCarbonFootprints = new List<GetCarbonFootprintDTOData>();
            
            if (carbonFootprints != null)
            {
                foreach (var carbonFootprint in carbonFootprints) {
                    getCarbonFootprints.Add(new GetCarbonFootprintDTOData
                    {
                        CarbonFootprintId = carbonFootprint.CarbonFootprintId,
                        AccountId= carbonFootprint.AccountId,
                        HouseholdConsumption = carbonFootprint.HouseholdConsumption,
                        NationalHouseholdConsumption = carbonFootprint.NationalHouseholdConsumption,
                        MonthOfAnalysis = carbonFootprint.MonthOfAnalysis,
                        YearOfAnalysis = carbonFootprint.YearOfAnalysis
                    });
                }
            }

            return new GetCarbonFootprintDTO
            {
                Data = getCarbonFootprints,
                Response = new ResponseInformation
                {
                    ServerMessage = statusMessage,
                    StatusCode = statusCode
                }
            };
        }

        public GetForecastChartDataDTO CreateGetForecastChartDataDTO(
            IEnumerable<ForecastChartData> forecastChartDatas,
            int statusCode,
            string statusMessage
        )
        {
            List<ForecastChartDataObjectDTO> getForecastChartDatas = new List<ForecastChartDataObjectDTO>();

            if (forecastChartDatas != null)
            {
                foreach (var forecastChart in forecastChartDatas)
                {

                    getForecastChartDatas.Add(new ForecastChartDataObjectDTO
                    {
                        ForecastChartDataId = forecastChart.ForecastChartDataId,
                        AccountId = forecastChart.AccountId,
                        TimespanType = forecastChart.TimespanType,
                        DateOfAnalysis= forecastChart.DateOfAnalysis,
                        Label = forecastChart.Label,
                        WattsValue = forecastChart.WattsValue,
                        PriceValue= forecastChart.PriceValue,
                        Index= forecastChart.Index,
                    });

                }
            }

            return new GetForecastChartDataDTO
            {
                Data = getForecastChartDatas,
                Response = new ResponseInformation
                {
                    ServerMessage = statusMessage,
                    StatusCode = statusCode
                }
            };
        }

    }
}

