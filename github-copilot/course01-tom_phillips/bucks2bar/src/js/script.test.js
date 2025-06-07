// filepath: src/js/script.test.js
describe('Username validation regex', () => {
    // The regex from script.js
    const regex = /^(?=.*[A-Z])(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$/;

    it('should pass for a valid username with uppercase, special char, and 8+ chars', () => {
        expect(regex.test('Valid!123')).toBe(true);
        expect(regex.test('A!bcdefg')).toBe(true);
        expect(regex.test('Password@1')).toBe(true);
    });

    it('should fail if missing uppercase letter', () => {
        expect(regex.test('invalid!123')).toBe(false);
        expect(regex.test('test@abcd')).toBe(false);
    });

    it('should fail if missing special character', () => {
        expect(regex.test('Invalid123')).toBe(false);
        expect(regex.test('Password1')).toBe(false);
    });

    it('should fail if less than 8 characters', () => {
        expect(regex.test('A!b2')).toBe(false);
        expect(regex.test('A!bcde')).toBe(false);
    });

    it('should fail if missing both requirements', () => {
        expect(regex.test('abcdefg')).toBe(false);
        expect(regex.test('12345678')).toBe(false);
    });

    it('should pass for multiple special characters and uppercase', () => {
        expect(regex.test('A!B@C#D$')).toBe(true);
    });

    it('should fail for empty string', () => {
        expect(regex.test('')).toBe(false);
    });
});