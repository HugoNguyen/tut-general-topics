import React from "react";
import drawCircle from "../utilities/canvasLoadAnimation";

function Mem({ data }) {
    const {
        totalMem,
        usedMem,
        memUseage,
        freeMem,
        memWidgetId,
    } = data;

    const canvas = document.querySelector(`[data-id="${memWidgetId}"]`);
    drawCircle(canvas, memUseage * 100);
    
    const totalMemInGB = Math.floor(totalMem/1073741824*100)/100;
    const freeMemInGB = Math.floor(freeMem/1073741824*100)/100;

    return (<>
        <div className="col-sm-3 mem">
            <h3>Memory Usage</h3>
            <div className="canvas-wrapper">
                <canvas data-id={memWidgetId} width="200" height="200"></canvas>
                <div className="mem-text">
                    {Math.floor(memUseage * 100)}%
                </div>
            </div>
            <div>
                Total Memory: {totalMemInGB}gb
            </div>
            <div>
                Free Memory: {freeMemInGB}gb
            </div>
        </div>
    </>)
}

export default Mem;