import React from "react";
import drawCircle from "../utilities/canvasLoadAnimation";

function Cpu({ data }) {
    const { cpuLoad, cpuWidgetId } = data;
    const canvas = document.querySelector(`[data-id="${cpuWidgetId}"]`);
    drawCircle(canvas, cpuLoad);
    return (<>
        <div className="col-sm-3 cpu">
            <h3>CPU load</h3>
            <div className="canvas-wrapper">
                <canvas data-id={cpuWidgetId} width="200" height="200"></canvas>
                <div className="cpu-text">{cpuLoad}%</div>
            </div>
        </div>
    </>)
}

export default Cpu;