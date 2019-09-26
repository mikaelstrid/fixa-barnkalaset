export class DateHelper {
    public static parseDate(dateString: string): Date {
        return new Date(dateString);
    }

    public static getTimeFromDateString(dateString: string): string {
        const date = DateHelper.parseDate(dateString);

        return `${('0' + date.getHours()).slice(-2)}:${('0' + date.getMinutes()).slice(-2)}`;
    }

    public static getMinutesTo(date: Date): number {
        const now = new Date();
        const diffInMilliseconds = date.getTime() - now.getTime();
        return diffInMilliseconds / 60000;
    }
}
