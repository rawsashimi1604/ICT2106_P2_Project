import React from "react";
import DeviceLogging from "components/DeviceLogging/DeviceLogging";
import DeviceEnergyLimitTable from "pages/DeviceEnergyLimit/EnergyLimitTable";
import EnergyLimitTable from "pages/DeviceEnergyLimit/EnergyLimitTable";
function Devices() {
    return (
        <div className="container mt-3">
            <DeviceLogging />
            <EnergyLimitTable/>
        </div>
    );
}
export default Devices;
