export function dateText(date: Date | string): string {
    if (typeof (date) === 'string') {
        return dateText(new Date(date));
    }

    return date.toLocaleDateString();
}