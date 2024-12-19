import fs from "fs/promises";
import { simplifySVG } from "flat-svg";
import { JSDOM } from "jsdom";
import formatXml from "xml-formatter";

export async function flattenSvg() {
    const { document } = new JSDOM(
        await fs.readFile("../../public/AggregateLogo.svg", "utf-8"),{
            pretendToBeVisual: true
        }).window;

    const svg = document.querySelector("svg");

    if (!svg) {
        throw new Error("No SVG found in the file");
    }

    const simplifiedSvg = simplifySVG(svg, {
        rasterizeAllMasks: false,
        keepGroupTransforms: false,
        vectorizeAllTexts: false,
    });

    const prettifiedSvg = formatXml(simplifiedSvg.outerHTML, {
        collapseContent: true,
    });

    await fs.writeFile("../../public/AggregateLogo-simplified.svg", prettifiedSvg);
}
