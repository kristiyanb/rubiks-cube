import { Row } from "./Row";

export interface ISideProps {
    side: string[][];
    className: string;
}

export const Side = ({ side, className }: ISideProps) => {
    const renderRows = () => {
        if (className == "face down") {
            const rows = [...side];
            rows.reverse();

            return rows.map((x, i) => {
                return <Row nodes={x} key={`${className}_${i}`} index={`${className}_${i}`} />
            })
        }

        return side.map((x, i) => {
            return <Row nodes={x} key={`${className}_${i}`} index={`${className}_${i}`} />
        });
    }
    return <div key={className} className={className}>
        {
            renderRows()
        }
    </div >
}